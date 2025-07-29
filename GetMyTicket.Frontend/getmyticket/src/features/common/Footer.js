import address from '../../assets/icons/address.png'
import email from '../../assets/icons/email.png'
import phone from '../../assets/icons/phone.png'

//TODO: add list and <a> effect when links are added

function Footer() {
  return (
    <section className="footer">
      <div className="footer__content">
        <div className="footer__section">
          <h4>About us</h4>
          <p>
            We make travel simple. Whether you're booking a flight, catching a
            train, or hopping on a bus, our platform brings all your travel
            options into one easy-to-use place. Compare, book, and manage your
            trips — all in one account.
          </p>
        </div>

        <div className="footer__section">
          <h4>Quick Links</h4>
          <ul>
            <li>Help</li>
            <li>FAQ</li>
            <li>Legal</li>
            <li>About us</li>
          </ul>
        </div>

        <address className="footer__section">
          <h4>Contact Info</h4>
          <ul>
            <li>
              <img src={address} alt='Icon'/>
              <span>9000 Varna, Bulgaria</span>
            </li>
            <li>
               <img src={phone} alt='Icon'/>
              <span>+359 893 000123 </span>
            </li>
            <li>
               <img src={email} alt='Icon'/>
              <span>info@example.com</span>
            </li>
          </ul>
        </address>
      </div>
      <footer>
        <span> Copyright &copy; 2025 by VOYAGO. All rights reserved. </span>
      </footer>
    </section>
  );
}

export default Footer;
