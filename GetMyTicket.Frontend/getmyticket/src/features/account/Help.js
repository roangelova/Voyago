import phone from "../../assets/icons/phone.png";
import chat from "../../assets/icons/chat.png";
import { useState } from "react";
import { toast } from "react-toastify";

export default function Help() {
  //TODO -> Once message is send, the contact form should disappear, a message should be created into the 'Notifiications' part and  the used
  //should get notified that send was successful and when he can expect an answer

  const [sendClicked, setSendClicked] = useState(false);

 function handleSendMessage(e){
    e.preventDefault();
    setSendClicked(true)
    toast.success("Message sent successfully. We will get back to you as soon as possible. Keep an eye on your notifications.")
  }

  return (
    <div className="help__container">
      <div className="help__heading">
        <h3 className="heading--tertiary">Contact out team</h3>
        <p>
          Got any questions regarding a booking or how our service works? We're
          here to help. Chat to our friendly team 24/7 and get answers quickly.
        </p>
      </div>
      <div className="help__left">
        <form onSubmit={handleSendMessage} className="help__contactForm">
          <h4 className="heading--quaternary">Shoot us an email </h4>
          <textarea
            type="text"
            name="text"
            minlength="10"
            maxlength="500"
            placeholder="Message"
          />
          <button disabled={sendClicked}  className="btn" type="submit">
            Send message
          </button>
        </form>
      </div>
      <div className="help__right">
        <ul>
          <li className="help__right--item">
            <h5>Call us</h5>
            <p>Call our team Mon-Fri 8am to 5pm.</p>
            <div>
              <img src={phone} alt="Phone icon"></img>
              <span>+359 893 000123</span>
            </div>
          </li>
          <li className="help__right--item">
            <h5>Chat with us</h5>
            <p>Speak to our friendly team via live chat</p>
            <ul>
              <div>
                <img src={chat} alt="Chat icon"></img>
                <span>Start a live chat</span>
              </div>
            </ul>
          </li>
        </ul>
      </div>
    </div>
  );
}
