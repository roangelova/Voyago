import "../../assets/style/css/style.css";

//TODO -> FIX TINY JUMP (NAV SHIFTS A BIT TO THE LEFT WHEN NOT IN HEADER)
function Header() {
  return (
    <div className="header">
      <div className="header__container">
        <div className="headings">
          <h1>Smart planning, smooth travel </h1>
          <p>
            All backed by our guarantee to give you the best experience possible
          </p>
        </div>
      </div>
    </div>
  );
}

export default Header;
