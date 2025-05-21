import { Outlet } from "react-router-dom";
import Footer from "../features/common/Footer";
import NavBar from "../features/common/NavBar";

function AppLayout() {
  return (
    <div className="appLayout">
      <NavBar />
      <main>
        <Outlet />
      </main>
      <Footer />
    </div>
  );
}

export default AppLayout;
