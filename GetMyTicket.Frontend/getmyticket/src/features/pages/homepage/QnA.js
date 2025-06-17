import { useState } from "react";
import arrowDown from "../../../assets/icons/arrowDown.png";
import arrowUp from "../../../assets/icons/arrowUp.png";

const questions = [
  {
    q: "Can I cancel or change my ticket after booking?",
    a: "Yes, but cancellation and change policies vary depending on the transport provider. Please check the specific terms on your ticket details or contact our support team for assistance.",
  },
  {
    q: "Do I need to print my ticket, or can I use a mobile version?",
    a: "In most cases, a mobile ticket is accepted. However, some providers may require a printed ticket, especially for trains and buses. Check your confirmation email for specific instructions.",
  },
  {
    q: "What should I do if I miss my bus/train/flight?",
    a: "Unfortunately, most transport providers do not offer refunds for missed departures. We recommend arriving at the departure point at least 30–60 minutes early. If you do miss your trip, contact our support to explore your options.",
  },
  {
    q: "Are there discounts for children, students, or seniors?",
    a: "Yes, many transport providers offer discounted fares for children, students, and seniors. During booking, select the appropriate passenger type to see if discounts apply.",
  },
  {
    q: "How will I receive my ticket after payment?",
    a: "Once your payment is confirmed, you will receive your ticket via email and/or SMS, depending on your preferences. You can also access your ticket through your account on our website.",
  },
  {
    q: "What happens if my trip is delayed or canceled due to bad weather?",
    a: "In case of delays or cancellations due to severe weather conditions, we follow the policy of the respective transport provider. You will be notified by email or SMS about any changes. If the trip is canceled, you may be eligible for a full refund or a free rebooking. Please contact our support team for assistance.",
  },
];

function QnA() {
  const [visible, setVisible] = useState(Array(questions.length).fill(false));

  const toggleVisibility = (index) => {
    setVisible((prev) =>
      prev.map((visibility, currIndex) => (currIndex === index ? !visibility : visibility))
    );
  };

  return (
    <section className="QnA">
      <h3 className="heading--tertiary">Need Help? Start Here!</h3>
      <ul>
        {questions.map((item, index) => (
          <li key={index}>
            <div className="QnA__question" onClick={() => toggleVisibility(index)}>
              <h4>{item.q}</h4>
              <img
                src={visible[index] ? arrowUp : arrowDown}
                alt={visible[index] ? "Arrow up icon" : "Arrow down icon"}
              />
            </div>
            {visible[index] && <p className="QnA__answer">{item.a}</p>}
          </li>
        ))}
      </ul>
    </section>
  );
}

export default QnA;
