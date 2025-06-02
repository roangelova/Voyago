import Nikolay from "../../../assets/images/Nikolay.jpg";
import Elina from "../../../assets/images/Elina.jpg";
import Marcus from "../../../assets/images/Marcus.jpg";
import Irina from "../../../assets/images/Irina.jpg";

function CustomerReviews() {
  return (
    <section className="reviews">
      <h2 className="heading--secondary">What our customers say about us</h2>
      <div className="reviews__container">
        <div className="reviews__card">
          <div className="reviews__card-imgWrapper">
            <img src={Nikolay} alt="An image of Nikolay S."></img>
          </div>
          <span>Nikolay S.</span>
          <p>
            "Heavy snow grounded my flight, and I thought I was stuck. The site
            suggested a train alternative — I made it on time. So grateful!"
          </p>
        </div>

        <div className="reviews__card">
          <div className="reviews__card-imgWrapper">
            <img src={Elina} alt="An image of Nikolay S."></img>
          </div>
          <span>Elina A.</span>
          <p>
            "Great service! I found a last-minute train ticket at a great
            price—saved me a ton of stress."
          </p>
        </div>

        <div className="reviews__card">
          <div className="reviews__card-imgWrapper">
            <img src={Marcus} alt="An image of Nikolay S."></img>
          </div>
          <span>Marcus R.</span>
          <p>
            "Booked a flight for a work trip — super fast and no hassle. The
            prices were competitive, and I appreciated how clear the whole
            process was from start to finish."
          </p>
        </div>

        <div className="reviews__card">
          <div className="reviews__card-imgWrapper">
            <img src={Irina} alt="An image of Nikolay S."></img>
          </div>
          <span>Irina K.</span>
          <p>
            "I accidentally booked the wrong date, but support responded fast
            and helped me fix it. Great service!"
          </p>
        </div>
      </div>
    </section>
  );
}

export default CustomerReviews;
