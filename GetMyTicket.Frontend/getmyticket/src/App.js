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
import AppLayout from './ui/AppLayout';
import Bookings from './features/account/Bookings';
import ProtectedRoute from './ui/ProtectedRoute';

const queryClient = new QueryClient();

function App() {

  return (
    <QueryClientProvider client={queryClient}>
      <ReactQueryDevtools initialIsOpen={false} />
      <BrowserRouter>
        <ToastContainer />
        <Routes>
          <Route element={<AppLayout />}>
            <Route index path="/" element={< MainPage />} />
            <Route path="/register" element={<RegisterPopUp />} />
            <Route path="/register-form" element={<RegisterForm />} />
            <Route path="/:searchType" element={<SearchByTypePage />} />
            <Route path="/search-results" element={<SearchResultsPage />} />

            <Route element={<ProtectedRoute />}>
              <Route path="/cart" element={<Cart />} />
              <Route path='/account'>
                <Route path='bookings' element={<Bookings />} />
                <Route path='profile' element={<p>my user profile</p>} />
              </Route>
            </Route>
          </Route>

          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;