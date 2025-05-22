import germany from "../../../assets/images/germany.jpg";
import italy from "../../../assets/images/italy.jpg";
import norway from "../../../assets/images/norway.jpg";
import spain from "../../../assets/images/spain.jpg";
import greece from "../../../assets/images/greece.jpg";
import bulgaria from "../../../assets/images/bulgaria.jpg";

//TODO: ADD A SMALL POPUP ON HOVER WITH DETAILS -> semi-transparent black background


function PopularDestinationsSection() {
  return (
    <section className="popularDestinations">
      <h2 className="heading--secondary margin-bottom--xs">
        Popular destinations
      </h2>
      <p className="margin-bottom--m">Not sure where to go next? Check out where everyone’s heading</p>
      <div className="popularDestinations__gallery margin-bottom--l">
        <div>
          <img className="popularDestinations__gallery--img1" src={germany} alt="Destination image of Germany" />
          <img className="popularDestinations__gallery--img2" src={italy} alt="Destination image of Italy" />
          <img className="popularDestinations__gallery--img3" src={norway} alt="Destination image of Norway" />
          <img className="popularDestinations__gallery--img4" src={spain} alt="Destination image of Spain" />
          <img className="popularDestinations__gallery--img5" src={greece} alt="Destination image of Greece" />
          <img className="popularDestinations__gallery--img6" src={bulgaria} alt="Destination image of Bulgaria" />
        </div>
      </div>
    </section >
  );
}

export default PopularDestinationsSection;
