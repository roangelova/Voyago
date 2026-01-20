import Header from "../common/Header";
import SearchBar from "../../ui/SearchBar";

import WhyUsSection from "./homepage/WhyUsSection";
import PopularDestinationsSection from "./homepage/PopularDestinationsSections";
import PartnerLogos from "./homepage/PartnerLogos";
import CustomerReviews from "./homepage/CustomerReviews";
import QnA from "./homepage/QnA";

const MainPage = () => {
    return (
        <>
            <Header />
            <PartnerLogos/>
            <CustomerReviews/>
            <WhyUsSection />
            <PopularDestinationsSection/>
            <QnA/>
        </>
    )
}

export default MainPage;