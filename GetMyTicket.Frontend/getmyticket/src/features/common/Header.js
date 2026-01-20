import "../../assets/style/css/style.css";
import SearchBar from "../../ui/SearchBar";

function Header() {
  return (
    <div className="header">
      <div className="header__container">
        <div className="header__headings">
          <h1 className="heading--primary">Smart planning, smooth travel </h1>
          <p>
            All backed by our guarantee to give you the best experience possible
          </p>
        </div>
        <SearchBar/>
      </div>
    </div>
  );
}

export default Header;
