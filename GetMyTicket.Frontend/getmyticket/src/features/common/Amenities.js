import bistro from "../../assets/icons/bistro.png";
import wc from "../../assets/icons/wc.png";
import highspeed from "../../assets/icons/highspeed.png";

function Amenities({ amenities: { hasToiletOnBoard, hasBistroOnBoard, isAHighspeedTrain } }) {
  return (
    <div className="resultCard__amenities">
      {hasBistroOnBoard && <img src={bistro} alt="Bistro on board" />}
      {isAHighspeedTrain && <img src={highspeed} alt="Highspeed connection" />}
      {hasToiletOnBoard && <img src={wc} alt="WC on board" />}
    </div>
  );
}

export default Amenities;
