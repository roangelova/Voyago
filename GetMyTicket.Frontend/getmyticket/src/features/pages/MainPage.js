import Header from "../common/Header";
import SearchBar from "../services/SearchBar";

import WhyUsSection from "./homepage/WhyUsSection";
import PopularDestinationsSection from "./homepage/PopularDestinationsSections";
import PartnerLogos from "./homepage/PartnerLogos";

const MainPage = () => {
    return (
        <>
            <Header />
            <SearchBar />
            <PartnerLogos/>
            <WhyUsSection />
            <PopularDestinationsSection/>
        </>
    )
}

export default MainPage;