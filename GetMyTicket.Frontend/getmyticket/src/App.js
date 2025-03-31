import { BrowserRouter, Route, Routes } from 'react-router-dom';

import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

import { ToastContainer } from 'react-toastify';

import RegisterPopUp from './features/account/RegisterPopUp';
import NotFoundPage from './features/common/NotFoundPage';
import RegisterForm from './features/account/RegisterForm';
import MainPage from './features/pages/MainPage';
import SearchResultsPage from './features/pages/SearchResultsPage';
import Cart from './features/cart/Cart';
import SearchByTypePage from './features/pages/SearchByTypePage';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';

const queryClient = new QueryClient();

function App() {

  return (
    <QueryClientProvider client={queryClient}>
      <ReactQueryDevtools initialIsOpen={false}/>
      <BrowserRouter>
        <ToastContainer />
        <Routes>
          <Route index path="/" element={< MainPage />} />
          <Route path="/register" element={<RegisterPopUp />} />
          <Route path="/cart" element={<Cart />} />
          <Route path="/:searchType" element={<SearchByTypePage />} />
          <Route path="/register-form" element={<RegisterForm />} />
          <Route path="/search-results" element={<SearchResultsPage />} />
          <Route path='/user' element={<p>User page</p>}>
            <Route path='bookings' element={<p>my user bookings</p>} />
            <Route path='profile' element={<p>my user profile</p>} />
          </Route>
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;