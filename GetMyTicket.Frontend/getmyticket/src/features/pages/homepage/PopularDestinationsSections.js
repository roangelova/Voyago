import germany from "../../../assets/images/germany.jpg";
import italy from "../../../assets/images/italy.jpg";
import norway from "../../../assets/images/norway.jpg";
import spain from "../../../assets/images/spain.jpg";
import greece from "../../../assets/images/greece.jpg";
import bulgaria from "../../../assets/images/bulgaria.jpg";

//TODO:ADJUST TEXT ON THE SPAN

function PopularDestinationsSection() {
  return (
    <section className="popularDestinations">
      <h2 className="heading--secondary margin-bottom--xs">
        Popular destinations
      </h2>
      <p className="margin-bottom--m">
        Not sure where to go next? Check out where everyone’s heading
      </p>

      <div className="popularDestinations__gallery margin-bottom--l">
        <div className="popularDestinations__grid">
          <div className="popularDestinations__destination popularDestinations__destination--1">
            <img src={germany} alt="Destination image of Germany" />
            <div className="popularDestinations__overlay">
              <h4 className="margin-bottom--xs">Germany</h4>
              <span className="margin-bottom--s">
                Discover vibrant cities, fairy-tale castles and scenic forests.
              </span>
              <a className="margin-bottom--s" href="#">Book now</a>
            </div>
          </div>

          <div className="popularDestinations__destination">
            <img src={italy} alt="Destination image of Italy" />
            <div className="popularDestinations__overlay">
              <h4 className="margin-bottom--xs">Italy</h4>
              <span className="margin-bottom--s">
                Dive into rich history, world-class cuisine and timeless beauty.
              </span>
              <a className="margin-bottom--s" href="#">Book now</a>
            </div>
          </div>

          <div className="popularDestinations__destination">
            <img
              className="popularDestinations__gallery--img3"
              src={norway}
              alt="Destination image of Norway"
            />
            <div className="popularDestinations__overlay">
              <h4 className="margin-bottom--xs">Norway</h4>
              <span className="margin-bottom--s">
                Explore majestic fjords, northern lights and peaceful nature.
              </span>
              <a className="margin-bottom--s" href="#">Book now</a>
            </div>
          </div>

          <div className="popularDestinations__destination">
            <img
              className="popularDestinations__gallery--img4"
              src={spain}
              alt="Destination image of Spain"
            />
            <div className="popularDestinations__overlay">
              <h4 className="margin-bottom--xs">Spain</h4>
              <span className="margin-bottom--s">
                Soak up the sun, savor tapas and embrace lively culture.
              </span>
              <a className="margin-bottom--s" href="#">Book now</a>
            </div>
          </div>

          <div className="popularDestinations__destination">
            <img
              className="popularDestinations__gallery--img5"
              src={greece}
              alt="Destination image of Greece"
            />
            <div className="popularDestinations__overlay">
              <h4 className="margin-bottom--xs">Greece</h4>
              <span className="margin-bottom--s">
                Wander through ancient ruins and relax by crystal-clear seas.
              </span>
              <a className="margin-bottom--s" href="#">Book now</a>
            </div>
          </div>

          <div className="popularDestinations__destination popularDestinations__destination--6">
            <img src={bulgaria} alt="Destination image of Bulgaria" />
            <div className="popularDestinations__overlay">
              <h4 className="margin-bottom--xs">Bulgaria</h4>
              <span className="margin-bottom--s">
                Enjoy hidden beaches, mountain escapes and cultural gems.
              </span>
              <a className="margin-bottom--s" href="#">Book now</a>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}

export default PopularDestinationsSection;
