// @@tagdynamic@@
// @@tagdepends: vle.discrete-time @@endtagdepends

#include <vle/DiscreteTime.hpp>
#include <CropDvtStage.hpp>

namespace vd = vle::devs;
namespace vv = vle::value;

namespace record {
namespace cv {

using namespace vle::discrete_time;

/**
 * @brief Croissance de la biomasse aerienne totale et de grain de la plante
 * par conversion de la lumière intercepté par les feuilles et utilisation d'un indice de recolte
 * Utilise l'extension DEVS DifferenceEquation::Multiple.
 * #include <CropDvtStage.hpp>
 *
 * Le modèle calcul à chaque pas de temps les variables d'état :
 * - AGBiomass : la biomasse aerienne totale de la plante. (g/m²)
 * - Yield : la biomasse de grain de la plante. (g/m²)
 * - HIpot : indice de recolte potentiel (-)
 * - HI : indice de recolte actuel (-)
 * - dayCount : nombre de jour depuis l'emergence (j)
 *
 * A partir des données d'entrée :
 * - Rad : la radiation solaire journalière totale (MJ/m²)
 * - ALAI : LAI actif (m²/m²)
 * - ATPT : le rapport transpiration réelle / potentielle utilisé comme indice de stress (-)
 * - DvtStage : le stade phenologique de la plante (cf CropDvtStage.hpp) (-)
 *
 * Et des paramètres :
 * - xtinc : le coefficient d'extinction de la lumière à travers les feuilles (-)
 * - Rue1 : RUE avant debut de senescence rapide des feuilles. (g/MJ)
 * - Rue2 : RUE après debut de senescence rapide des feuilles. (g/MJ)
 * - r1Rue : effet du rapport ATPT sur la croissance en biomasse (-)
 * - r2Rue : effet du rapport ATPT sur la croissance en biomasse (-)
 * - hiMax : indice de recolte maximal (-)
 * - r1hi : effet du rapport ATPT sur l'indice de recolte (-)
 * - r2hi : effet du rapport ATPT sur l'indice de recolte (-)
 * - rateHI : croissance journaliere maximale du HI(/j)
 */
class CropAGB: public DiscreteTimeDyn {
public:
    /**
     * @brief Constructeur de la classe du modèle.
     * C'est ici que se font les enregistrements des variables d'état (Var)
     * et des variables d'entrées (Sync & Var) dans le moteur de simulation VLE.
     * C'est aussi ici que l'ont attribut leur valeurs aux paramètres du modèle à
     * partir des conditions expérimentales définies dans le VPZ
     *
     * @param events liste des evenements provenant des conditions expérimentales du VPZ
     * @param atom ?
     */
    CropAGB(const vd::DynamicsInit& init, const vd::InitEventList& events) :
        DiscreteTimeDyn(init, events)
    {

        // Lecture des valeurs de parametres dans les conditions du vpz
        //ces parametres ont une valeur par defaut utilise si la condition n'est pas definie
        xtinc = (events.exist("xtinc")) ? vv::toDouble(events.get("xtinc")) : 0.7;
        Rue1 = (events.exist("Rue1")) ? vv::toDouble(events.get("Rue1")) : 2.8;
        Rue2 = (events.exist("Rue2")) ? vv::toDouble(events.get("Rue2")) : 1.5;
        r1Rue = (events.exist("r1Rue")) ? vv::toDouble(events.get("r1Rue")) : 1.0;
        r2Rue = (events.exist("r2Rue")) ? vv::toDouble(events.get("r2Rue")) : 1.0;
        hiMax = (events.exist("hiMax")) ? vv::toDouble(events.get("hiMax")) : 0.55;
        r1hi = (events.exist("r1hi")) ? vv::toDouble(events.get("r1hi")) : 0.6;
        r2hi = (events.exist("r2hi")) ? vv::toDouble(events.get("r2hi")) : 0.8;
        rateHI = (events.exist("rateHI")) ? vv::toDouble(events.get("rateHI")) : 0.015;

        // Variables d'etat gerees par ce composant
        AGBiomass.init(this,"AGBiomass", events);
        Yield.init(this,"Yield", events);
        HIpot.init(this,"HIpot", events);
        HI.init(this,"HI", events);
        dayCount.init(this,"dayCount", events);

        // Variables d'entree gerees par un autre composant
        Rad.init(this,"Rad", events);
        ALAI.init(this,"ALAI", events);
        ATPT.init(this,"ATPT", events);
        DvtStage.init(this,"DvtStage", events);
    }

    /**
     * @brief Destructeur de la classe du modèle.
    **/
    virtual ~CropAGB() { };

    /**
     * @brief Methode de calcul effectuée à chaque pas de temps
     * @param time la date du pas de temps courant
     */
    virtual void compute(const vd::Time& /*time*/)
    {
        if (DvtStage() == BARE_SOIL) {
            AGBiomass = 0;
            Yield = 0.0;
            HI = 0;
            HIpot = 0;
            dayCount = 0;
        } else {
            double Rue = 0;
            switch ((int) DvtStage()) {
                case EMERGENCE:
                case MAX_LAI:
                case FLOWERING:
                case CRITICAL_GRAIN_ABORTION:
                    Rue = Rue1;
                    break;
                case LEAF_SENESCENCE:
                case MATURITY:
                case SOWING:
                    Rue = Rue2;
                    break;
            }
            double dAGB = 0.48 * Rad() * Rue * (1 - exp(-xtinc * ALAI()))
                    * reduc(ATPT(-1), r1Rue, r2Rue);
            AGBiomass = AGBiomass(-1) + dAGB;
            switch ((int) DvtStage()) {
                case SOWING:
                case EMERGENCE:
                    HIpot = 0;
                    dayCount = 0;
                    break;
                case MAX_LAI:
                case FLOWERING:
                    dayCount = dayCount(-1) + 1;
                    HIpot = (HIpot(-1) * dayCount(-1) + hiMax * reduc(ATPT(-1),
                            r1hi, r2hi)) / dayCount();
                    break;
                case CRITICAL_GRAIN_ABORTION:
                case LEAF_SENESCENCE:
                case MATURITY:
                    HIpot = HIpot(-1);
                    dayCount = dayCount(-1);
                    break;
            }
            double dHI = 0;
            double buff = HIpot() - HI(-1);
            switch ((int) DvtStage()) {
                case SOWING:
                case EMERGENCE:
                case MAX_LAI:
                case FLOWERING:
                    dHI = 0;
                    break;
                case CRITICAL_GRAIN_ABORTION:
                case LEAF_SENESCENCE:
                case MATURITY:
                    dHI = std::min(rateHI, buff);
                    break;
            }
            HI = HI(-1) + dHI;
            switch ((int) DvtStage()) {
                case SOWING:
                case EMERGENCE:
                case MAX_LAI:
                case FLOWERING:
                case CRITICAL_GRAIN_ABORTION:
                case LEAF_SENESCENCE:
                    Yield = Yield(-1);
                    break;
                case MATURITY:
                    Yield = HI() * AGBiomass();
                    break;
            }
        }
    }

private:
    //Variables d'etat
    /**
     * @brief la biomasse aerienne totale de la plante. (g/m²)
     */
    Var AGBiomass;
    /**
     * @brief la biomasse de grain de la plante. (g/m²)
     */
    Var Yield;
    /**
     * @brief indice de recolte potentiel (-)
     */
    Var HIpot;
    /**
     * @brief indice de recolte actuel (-)
     */
    Var HI;
    /**
     * @brief nombre de jour depuis l'emergence (j)
     */
    Var dayCount;

    //Entrées
    /**
     * @brief a radiation solaire journalière totale (MJ/m²)
     */
    Var Rad;
    /**
     * @brief LAI actif (m²/m²)
     */
    Var ALAI;
    /**
     * @brief le rapport transpiration réelle / potentielle utilisé comme indice de stress (-)
     */
    Var ATPT;
    /**
     * @brief le stade phenologique de la plante (cf CropDvtStage.hpp) (-)
     */
    Var DvtStage;

    //Parametres du modele
    /**
     * @brief le coefficient d'extinction de la lumière à travers les feuilles (-)
     */
    double xtinc;
    /**
     * @brief RUE avant debut de senescence rapide des feuilles. (g/MJ)
     */
    double Rue1;
    /**
     * @brief RUE après debut de senescence rapide des feuilles. (g/MJ)
     */
    double Rue2;
    /**
     * @brief effet du rapport ATPT sur la croissance en biomasse (-)
     */
    double r1Rue;
    /**
     * @brief effet du rapport ATPT sur la croissance en biomasse (-)
     */
    double r2Rue;
    /**
     * @brief indice de recolte maximal (-)
     */
    double hiMax;
    /**
     * @brief effet du rapport ATPT sur l'indice de recolte (-)
     */
    double r1hi;
    /**
     * @brief effet du rapport ATPT sur l'indice de recolte (-)
     */
    double r2hi;
    /**
     * @brief croissance journaliere maximale du HI(/j)
     */
    double rateHI;

    //fonction locale
    /**
     * @brief fonction de reduction utilisée avec l'indice de stress ATPT
     * @param x la valeur du stress
     * @param p1 parametre de controle
     * @param p2 parametre de controle
     * @return valeur de la reduction a appliquer
     */
    double reduc(double x, double p1, double p2)
    {
        if (x < p2 - p1) {
            return 0;
        } else {
            if (x > p2) {
                return 1;
            } else {
                return (p1 - p2 + x) / p1;
            }
        }
    }

};
}
}//namespaces
DECLARE_DYNAMICS(record::cv::CropAGB); // balise specifique VLE

