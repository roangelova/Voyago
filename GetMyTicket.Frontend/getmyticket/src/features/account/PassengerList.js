import { useAccountContext } from "./AccountContext";

export default function PassengerList() {
  const data = useAccountContext();
  console.log(data);

  return (
    <div className="passengers">
      <h4 className="heading--quaternary">Passengers:</h4>

      <div className="passengers__content">
        <div className="passengers__row">
          <div>
            <span className="passengers--name">Rachel Green</span>
            <span>
              <strong>Adult</strong>
            </span>
          </div>

          <span>10.07.1997</span>
        </div>

        <div className="passengers__row">
          <div>
            <span className="passengers--name">Ross Geller</span>
            <span>
              <strong>Adult</strong>
            </span>
          </div>
          <span>15.07.1990</span>
        </div>
      </div>
    </div>
  );
}
