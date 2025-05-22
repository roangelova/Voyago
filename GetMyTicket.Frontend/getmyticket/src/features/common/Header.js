import "../../assets/style/css/style.css";

//TODO -> FIX TINY JUMP (NAV SHIFTS A BIT TO THE LEFT WHEN NOT IN HEADER)
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
      </div>
    </div>
  );
}

export default Header;
