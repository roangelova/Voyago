import Header from "../common/Header";
import SearchBar from "../services/SearchBar";
import Footer from "../common/Footer";

import { useState } from "react";

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

            <Footer />
        </>
    )
}

export default MainPage;