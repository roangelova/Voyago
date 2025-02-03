import { BrowserRouter, Route, Routes } from 'react-router';

import Header from './components/common/Header';
import RegisterForm from './components/account/RegisterForm';
import RegisterPopUp from './components/account/RegisterPopUp';
import NotFoundPage from './components/common/NotFoundPage';
import MainPage from './components/pages/MainPage';
import NavBar from './components/common/NavBar';
import SearchResultsPage from './components/pages/SearchResultsPage';

function App() {
  return (

    //TODO -> how are we gonna structure the use of header?
    //should always be in the mainpage, not when registering etc

    <BrowserRouter>

      <Routes>
        <Route index path="/" element={< SearchResultsPage />} />
        <Route path="/register" element={<RegisterPopUp />} />
        <Route path="/register-form" element={<RegisterForm />} />
        <Route path="*" element={<NotFoundPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;


//Structure (HOMEPAGE): 
//
//Header 60VH 
// Search Bar -> should overlap a tiny portion of the header
// CTA section Logos -> display provider logos and link to main booking page
// WHY US SECTION -> 3 reasons to choose us
// ..
// ..
//ABOUT US & FOOTER 