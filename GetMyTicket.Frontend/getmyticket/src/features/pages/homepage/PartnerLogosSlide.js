import deutschebahn from "../../../assets/logos/deutschebahn.svg";
import eurostar from "../../../assets/logos/eurostar.svg";
import flixbus from "../../../assets/logos/flixbus.svg";
import obb from "../../../assets/logos/obb.svg";
import skyteam from "../../../assets/logos/skyteam.svg";
import sncf from "../../../assets/logos/sncf.svg";
import staralliance from "../../../assets/logos/staralliance.svg";
import trenitalia from "../../../assets/logos/trenitalia.svg";
import wizzair from "../../../assets/logos/wizzair.svg";
import lufthansa from "../../../assets/logos/lufthansa.svg";
import sas from "../../../assets/logos/sas.svg";
import airfrance from "../../../assets/logos/airfrance.svg";
import klm from "../../../assets/logos/klm.svg";

function PartnerLogosSlide() {
  return (
    <div className="partnerLogos__slide">
      <img src={deutschebahn} alt="Deutsche Bahn logo" />
      <img src={eurostar} alt="Eurostarlogo" />
      <img src={flixbus} alt="Flixbus logo" />
      <img src={obb} alt="Obb logo" />
      <img src={skyteam} alt="Skyteam logo" />
      <img src={sncf} alt="SNCFlogo" />
      <img src={staralliance} alt="Star alliance logo" />
      <img src={trenitalia} alt="TrenItalia logo" />
      <img src={wizzair} alt="Wizz air logo" />
      <img src={sas} alt="SAS logo" />
      <img src={lufthansa} alt="Lufthansa logo" />
      <img src={airfrance} alt="Air France logo" />
      <img src={klm} alt="KLM logo" />
    </div>
  );
}

export default PartnerLogosSlide;
