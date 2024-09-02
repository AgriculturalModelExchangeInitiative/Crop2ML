// @@tagdynamic@@
// @@tagdepends: vle.discrete-time @@endtagdepends

#include <vle/DiscreteTime.hpp> 

namespace vd = vle::devs;

namespace vv = vle::value;

namespace record {
namespace cv {
using namespace vle::discrete_time;

/**
 * @brief Calculate Photoperiod Factor.
 * Utilise l'extension DEVS DifferenceEquation::Multiple.
 *
 **/
class PhotoperiodFactor : public DiscreteTimeDyn
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
    PhotoperiodFactor(const vd::DynamicsInit& atom, const vd::InitEventList& events)
        : DiscreteTimeDyn(atom, events)
    {
        //attribut les valeurs des parametres a partir des conditions du vpz
        PhotoInhibition = vv::toDouble(events.get("PhotoInhibition"));
        PhotoInsensitivity = vv::toDouble(events.get("PhotoInsensitivity"));
        //ces parametres ont une valeur par defaut utilise si la condition n'est pas definie
        PhotoInhibition = (events.exist("PhotoInhibition")) ?
            vv::toDouble(events.get("PhotoInhibition")) : 14;
        PhotoInsensitivity = (events.exist("PhotoInsensitivity")) ?
            vv::toDouble(events.get("PhotoInsensitivity")) : 6;
 
        //enregistrement des variables d'état et d'entrée du modèle
        DayLength.init(this,"DayLength", events);
        
        PhotoPeriodFactor.init(this,"DvtStage", events);
   

        DayLength.init_value(12);
     
    }

    /**
     * @brief Destructeur de la classe du modèle.
    **/
    virtual ~PhotoperiodFactor() {}

    /**
     * @brief Methode de calcul effectuée à chaque pas de temps
     * @param time la date du pas de temps courant
     */
    virtual void compute(const vd::Time& /*time*/) {
				double Dl = Daylenght();                
                double DlIns = PhotoInsensitivity;
                double DlInb = PhotoInhibition;
                double PhotoPeriodFactor;

                PhotoPeriodFactor = (DlInb - Dl) / (DlInb - DlIns);
                if (DlIns > DlInb)
                {
                    PhotoPeriodFactor = (Dl - DlInb) / (DlIns - DlInb);
                }
                if (PhotoPeriodFactor < 0)
                {
                    PhotoPeriodFactor = 0;
                }
                if (PhotoPeriodFactor > 1)
                {
                    PhotoPeriodFactor = 1;
                } 
    }

private:
    //paramètres
 
    double PhotoInhibition;
 
    double PhotoInsensitivity;

    Var DayLength;
 
    Var PhotoPeriodFactor; //
  
};

}
} // namespace record::cv

DECLARE_DYNAMICS(record::cv::PhotoperiodFactor)

