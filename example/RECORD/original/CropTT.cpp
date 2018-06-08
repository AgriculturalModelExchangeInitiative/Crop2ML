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
 * @brief Calcul de l'avancement du temps thermique et des stades phenologiques de la plantes.
 * Utilise l'extension DEVS DifferenceEquation::Multiple.
 *
 * Le modèle calcul à chaque pas de temps les variables d'état :
 * - TT : temps thermique (°C.j)
 * - DvtStage : le stade phenologique de la plante (cf CropDvtStage.hpp) (-)
 *
 * A partir des données d'entrée :
 * - Tmin : la température minimale journalière (°C)
 * - Tmax : la température maximale journalière (°C)
 * - tt_em_mat : le temps thermique entre emergence et maturité (°C.j)
 *
 * Et des paramètres :
 * - tt_Sowing_Emergence : le temps thermique entre les stades 0 & 1 (°C.j)
 * - tt_Emergence_MaxLAI : le temps thermique entre les stades 1 & 2 (°C.j)
 * - tt_MaxLAI_Flowering : le temps thermique entre les stades 2 & 3 (°C.j)
 * - tt_Flowering_GrainAbort : le temps thermique entre les stades 3 & 4 (°C.j)
 * - tt_GrainAbort_LeafSen : le temps thermique entre les stades 4 & 5 (°C.j)
 * - tt_LeafSen_Maturity : le temps thermique entre les stades 5 & 6 (°C.j)
 */
class CropTT : public DiscreteTimeDyn
{
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
    CropTT(const vd::DynamicsInit& atom, const vd::InitEventList& events)
        : DiscreteTimeDyn(atom, events)
    {
        //attribut les valeurs des parametres a partir des conditions du vpz
        tt_Emergence_MaxLAI = vv::toDouble(events.get("tt_Emergence_MaxLAI"));
        tt_LeafSen_Maturity = vv::toDouble(events.get("tt_LeafSen_Maturity"));
        //ces parametres ont une valeur par defaut utilise si la condition n'est pas definie
        tt_Sowing_Emergence = (events.exist("tt_Sowing_Emergence")) ?
            vv::toDouble(events.get("tt_Sowing_Emergence")) : 80.0;
        tt_MaxLAI_Flowering = (events.exist("tt_MaxLAI_Flowering")) ?
            vv::toDouble(events.get("tt_MaxLAI_Flowering")) : 90.0;
        tt_Flowering_GrainAbort = (events.exist("tt_Flowering_GrainAbort")) ?
            vv::toDouble( events.get("tt_Flowering_GrainAbort")) : 250.0;
        tt_GrainAbort_LeafSen = (events.exist("tt_GrainAbort_LeafSen")) ?
            vv::toDouble(events.get("tt_GrainAbort_LeafSen")) : 245.0;

        //enregistrement des variables d'état et d'entrée du modèle
        TT.init(this,"TT", events);
        DvtStage.init(this,"DvtStage", events);
        Tmin.init(this,"Tmin", events);
        Tmax.init(this,"Tmax", events);
        Sowing.init(this,"Sowing", events);
        Harvesting.init(this,"Harvesting", events);
        tt_em_mat.init(this,"tt_em_mat", events);

        DvtStage.init_value((double) BARE_SOIL);
        tt_em_mat.init_value(tt_Emergence_MaxLAI + tt_MaxLAI_Flowering
                        + tt_Flowering_GrainAbort + tt_GrainAbort_LeafSen
                        + tt_LeafSen_Maturity);
    }

    /**
     * @brief Destructeur de la classe du modèle.
    **/
    virtual ~CropTT() {}

    /**
     * @brief Methode de calcul effectuée à chaque pas de temps
     * @param time la date du pas de temps courant
     */
    virtual void compute(const vd::Time& /*time*/) {
        Sowing = 0;
        Harvesting = 0;

        //variable locale temporaire pour le calcul de TT
        double TTtemp = TT(-1) + std::max(0., (Tmin() + std::min(30., Tmax() )) / 2 - 6);
        switch ((State) DvtStage(-1)) {
        case BARE_SOIL: {
            if (Sowing() == 1.0) {
                DvtStage = SOWING;
                TT = TTtemp;
            } else {
                TT = 0;
            }

            break;
        } case SOWING : {
            if (TTtemp >= tt_Sowing_Emergence) {
                TT = TTtemp - tt_Sowing_Emergence;//reinitialisation de TT à l'emergence
                DvtStage = EMERGENCE;
            } else {
                TT = TTtemp;
            }
            break;
        } case EMERGENCE : {
            TT = TTtemp;
            if (TT() >= tt_Emergence_MaxLAI) {
                DvtStage = MAX_LAI;
            }
            break;
        } case MAX_LAI: {
            TT = TTtemp;
            if (TT() >= tt_Emergence_MaxLAI + tt_MaxLAI_Flowering) {
                DvtStage = FLOWERING;
            }
            break;
        } case FLOWERING: {
            TT = TTtemp;
            if (TT() >= tt_Emergence_MaxLAI+tt_MaxLAI_Flowering+tt_Flowering_GrainAbort){
                DvtStage = CRITICAL_GRAIN_ABORTION;
            }
            break;
        } case CRITICAL_GRAIN_ABORTION: {
            TT = TTtemp;
            if ((TT() >= tt_Emergence_MaxLAI + tt_MaxLAI_Flowering +
                    tt_Flowering_GrainAbort + tt_GrainAbort_LeafSen)) {
                DvtStage = LEAF_SENESCENCE;
            }
            break;
        } case LEAF_SENESCENCE: {
            TT = TTtemp;
            if (TT() >= tt_Emergence_MaxLAI + tt_MaxLAI_Flowering +
                    tt_Flowering_GrainAbort + tt_GrainAbort_LeafSen +
                    tt_LeafSen_Maturity) {
                DvtStage = MATURITY;
            }
            break;
        } case MATURITY: {
            if (Harvesting() == 1.0) {
                DvtStage = BARE_SOIL;
                TT = 0;
            }
            break;
        }}
    }

private:
    //paramètres
    /**
     * @brief temps thermique entre les stades : SOWING et EMERGENCE (respectivement 0 & 1) (°C.j)
     */
    double tt_Sowing_Emergence;
    /**
     * @brief temps thermique entre les stades : EMERGENCE et MAX_LAI (respectivement 1 & 2) (°C.j)
     */
    double tt_Emergence_MaxLAI;
    /**
     * @brief temps thermique entre les stades : MAX_LAI et FLOWERING (respectivement 2 & 3) (°C.j)
     */
    double tt_MaxLAI_Flowering;
    /**
     * @brief temps thermique entre les stades : FLOWERING et CRITICAL_GRAIN_ABORTION (respectivement 3 & 4) (°C.j)
     */
    double tt_Flowering_GrainAbort;
    /**
     * @brief temps thermique entre les stades : CRITICAL_GRAIN_ABORTION et LEAF_SENESCENCE (respectivement 4 & 5) (°C.j)
     */
    double tt_GrainAbort_LeafSen;
    /**
     * @brief temps thermique entre les stades : LEAF_SENESCENCE et MATURITY (respectivement 5 & 6) (°C.j)
     */
    double tt_LeafSen_Maturity;
    //variables d'état
    /**
     * @brief temps thermique (°C.j)
     */
    Var TT;
    /**
     * @brief stades phénologique de la plante (cf CropDvtStage.hpp) {0:SOWING - 1:EMERGENCE - 2:MAX_LAI - 3:FLOWERING - 4:CRITICAL_GRAIN_ABORTION - 5:LEAF_SENESCENCE - 6:MATURITY
     */
    Var DvtStage; //
    //variable d'entrée
    /**
     * @brief température journalière minimale (°C)
     */
    Var Tmin;
    /**
     * @brief température journalière maximale (°C)
     */
    Var Tmax;

    /**
     * @brief temps thermique entre emergence et maturité (°C.j)
     */
    Var tt_em_mat;

    Var Sowing;
    Var Harvesting;
};

}
} // namespace record::cv

DECLARE_DYNAMICS(record::cv::CropTT)

