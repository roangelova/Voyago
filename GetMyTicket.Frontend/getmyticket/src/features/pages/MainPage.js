import Header from "../common/Header";
import SearchBar from "../services/SearchBar";
import Footer from "../common/Footer";

import { useState } from "react";
import WhyUsSection from "../common/WhyUsSection";

const MainPage = () => {
    const [LoginPopupVisibility, setLoginPopupVisibility] = useState(false);

    const handleLoginToggle = () => {
        setLoginPopupVisibility(true);
    };

    return (
        <>
            <Header
                LoginPopupVisibility={LoginPopupVisibility}
                handleLoginToggle={handleLoginToggle}
                setLoginPopupVisibility={setLoginPopupVisibility} />
            <SearchBar LoginPopupVisibility={LoginPopupVisibility} />
            <WhyUsSection/>
            <Footer />
        </>
    )
}

export default MainPage;