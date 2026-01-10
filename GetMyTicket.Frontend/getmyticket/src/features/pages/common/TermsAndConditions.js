const termsSections = [
  {
    title: "About VOYAGO",
    content: (
      <>
        <p>
          VOYAGO is a <strong>training and educational project</strong> created
          for learning and demonstration purposes only.
        </p>
        <p>
          VOYAGO does <strong>NOT</strong> operate as a licensed travel agency.
          VOYAGO does <strong>NOT</strong> directly sell or issue transportation
          tickets.
        </p>
        <p>
          Any ticket prices, schedules, routes, or availability shown are{" "}
          <strong>simulated or illustrative</strong>.
        </p>
      </>
    ),
  },
  {
    title: "Accounts and User Information",
    content: (
      <p>
        To access certain features, you may be required to create an account.
      </p>
    ),
  },
  {
    title: "Ticket Information and Pricing",
    content: (
      <>
        <ul>
          <li>
            Ticket information is provided for{" "}
            <strong>training and demonstration purposes</strong>.
          </li>
          <li>
            Prices, availability, routes, and schedules may be inaccurate,
            incomplete, or outdated.
          </li>
          <li>
            VOYAGO does <strong>NOT</strong> guarantee the correctness of any
            travel-related information.
          </li>
        </ul>
        <p>
          VOYAGO is <strong>NOT</strong> responsible for decisions made based on
          the information displayed on the platform.
        </p>
      </>
    ),
  },
  {
    title: "Payments",
    content: (
      <ul>
        <li><strong>No</strong> real tickets are issued</li>
        <li>Payments may be processed in test or sandbox mode only</li>
        <li>
          Payment features are provided solely for demonstration or development
          purposes
        </li>
      </ul>
    ),
  },
  {
    title: "Cancellations and Refunds",
    content: (
      <ul>
        <li>Real cancellations or refunds are <strong>not</strong> applicable</li>
        <li>Cancellation or refund functionality is simulated</li>
        <li>
          <strong>No</strong> contractual relationship is formed with transport
          providers
        </li>
      </ul>
    ),
  },
  {
    title: "Intellectual Property",
    content: (
      <>
        <p>
          All content on VOYAGO, including text, designs, code, and user
          interface elements, is the property of VOYAGO or its creator.
        </p>
      </>
    ),
  },
  {
    title: "Third-Party Services and Links",
    content: (
      <ul>
        <li>VOYAGO does <strong>NOT</strong> control third-party services</li>
        <li>
          VOYAGO is <strong>NOT</strong> responsible for their content, accuracy,
          or availability
        </li>
        <li>VOYAGO does <strong>NOT</strong> endorse any third-party services</li>
      </ul>
    ),
  },
  {
    title: "Disclaimer of Warranties",
    content: (
      <>
        <p>
          The Service is provided <strong>"as is"</strong> and{" "}
          <strong>"as available"</strong>.
        </p>
        <p>Your use of the Service is at your own risk.</p>
      </>
    ),
  },
  {
    title: "Limitation of Liability",
    content: (
      <ul>
        <li>Direct or indirect damages</li>
        <li>Loss of data</li>
        <li>Loss of profits</li>
        <li>Travel-related losses or decisions</li>
      </ul>
    ),
  },
  {
    title: "Termination",
    content: (
      <p>
        VOYAGO reserves the right to suspend, terminate, modify, or discontinue
        the Service at any time without notice.
      </p>
    ),
  },
  {
    title: "Changes to These Terms",
    content: (
      <p>
        Continued use of the Service after changes constitutes acceptance of
        the updated Terms.
      </p>
    ),
  },
];


function TermsAndConditions() {
  return (
    <div className="termsAndConditions">
      <h2 className="heading--secondary">Terms and Conditions</h2>

      <p>
        VOYAGO is a training and demonstration platform that allows users to
        search for and simulate the purchase of bus, train, and plane tickets.
      </p>

      {termsSections.map((section, index) => (
        <section key={section.title}>
          <h4>
            {index + 1}. {section.title}
          </h4>
          {section.content}
        </section>
      ))}

      <p>
        <em>
          These Terms and Conditions are provided for educational and training
          purposes and do <strong>NOT</strong> constitute legal advice.
        </em>
      </p>
    </div>
  );
}

export default TermsAndConditions;
