import { BrowserRouter, Route, Routes } from 'react-router';

import Header from './components/common/Header';
import RegisterForm from './components/account/RegisterForm';
import RegisterPopUp from './components/account/RegisterPopUp';
import { ToastContainer } from 'react-toastify';
import NotFoundPage from './components/common/NotFoundPage';

function App() {
  return (

    <div >
      <BrowserRouter>
        <Routes>
          <Route index path="/" element={<Header />} />
          <Route path="/register" element={<RegisterPopUp />} />
          <Route path="/register-form" element={<RegisterForm />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </BrowserRouter>
    </div >
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