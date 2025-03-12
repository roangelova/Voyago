import support from '../../assets/icons/support.png';
import refund from '../../assets/icons/refund.png';
import reward from '../../assets/icons/reward.png';
import reliable from '../../assets/icons/reliable.png';


function WhyUsSection() {
    return (
        <section className='whyUs_container'>
            <h2>The Best Travel Experience, Guaranteed</h2>
            <ul>
                <li className='whyUs_container__item'>
                    <h3>Reliable</h3>
                    <img src={reliable} alt='Reliable icon' />
                    <p>Real-time updates about your bookings. Instant alerts for delays, gate changes and cancellations.</p>
                </li>

                <li className='whyUs_container__item'>
                    <h3>Best price guarantee</h3>
                    <img src={reward} alt='Best price quarantee icon' />
                    <p>If you find a trip cheaper somewhere else, we will credit the price difference to your account</p>
                </li>

                <li className='whyUs_container__item'>
                    <h3>24/7 Support</h3>
                    <img src={support} alt='24/7 support icon' />
                    <p>Travel care-free with our 24/7 support - availale by phone, email and chat </p>
                </li>

                <li className='whyUs_container__item'>
                    <h3>Flexible Cancellation Policy</h3>
                    <img src={refund} alt='Refund policy icon' />
                    <p>Options for changes and refunds with minimal hassle. Because we understand that somethimes thing happen.</p>
                </li>
            </ul>
        </section>
    )
}

export default WhyUsSection;