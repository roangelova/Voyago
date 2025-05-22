import Header from "../common/Header";
import SearchBar from "../services/SearchBar";

import WhyUsSection from "./homepage/WhyUsSection";
import PopularDestinationsSection from "./homepage/PopularDestinationsSections";

const MainPage = () => {
    return (
        <>
            <Header />
            <SearchBar />
            <WhyUsSection />
            <PopularDestinationsSection/>
        </>
    )
}

export default MainPage;