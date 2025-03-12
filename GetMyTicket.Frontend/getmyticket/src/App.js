import { BrowserRouter, Route, Routes } from 'react-router-dom';

import RegisterPopUp from './features/account/RegisterPopUp';
import NotFoundPage from './features/common/NotFoundPage';
import RegisterForm from './features/account/RegisterForm';
import MainPage from './features/pages/MainPage';
import SearchResultsPage from './features/pages/SearchResultsPage';
import { ToastContainer } from 'react-toastify';
import Cart from './features/cart/Cart';

function App() {
  return (
    <BrowserRouter>
      <ToastContainer />
      <Routes>
        <Route index path="/" element={< MainPage />} />
        <Route path="/register" element={<RegisterPopUp />} />
        <Route path="/cart" element={<Cart />} />
        <Route path="/register-form" element={<RegisterForm />} />
        <Route path="/search-results" element={<SearchResultsPage />} />
        <Route path='/user' element={<p>User page</p>}>
          <Route path='bookings' element={<p>my user bookings</p>}/>
          <Route path='profile' element={<p>my user profile</p>}/>
        </Route>
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