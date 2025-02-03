import Header from "../common/Header";
import SearchBar from "../services/SearchBar";
import Footer from "../common/Footer";

const MainPage = () => {
    //TODO -> amend LoginPopUp so that both header and Searchbar are blurred when login is showing;
    return (
        <>
            <Header />


            <SearchBar />

            <Footer />
        </>
    )
}

export default MainPage;