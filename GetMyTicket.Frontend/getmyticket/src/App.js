import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";

import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

import { ToastContainer } from "react-toastify";

import NotFoundPage from "./features/common/NotFoundPage";
import MainPage from "./features/pages/MainPage";
import SearchResultsPage from "./features/pages/SearchResultsPage";
import Cart from "./features/cart/Cart";
import SearchByTypePage from "./features/pages/SearchByTypePage";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import AppLayout from "./ui/AppLayout";
import ProtectedRoute from "./ui/ProtectedRoute";
import BillingPage from './features/account/billing/BillingPage'
import Account from "./features/pages/Account";
import AccountDashboard from "./features/account/AccountDashboard";
import BookingDetails from "./features/bookings/BookingDetails";
import PassengerList from "./features/account/passengers/PassengerList";
import Help from "./features/account/help/Help";
import Bookings from "./features/bookings/Bookings";
import Notifications from "./features/account/notifications/Notifications";
import TermsAndConditions from "./features/pages/common/TermsAndConditions";
const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <ReactQueryDevtools initialIsOpen={false} />
      <BrowserRouter>
        <ToastContainer />
        <Routes>
          <Route element={<AppLayout />}>
            <Route index path="/" element={<MainPage />} />

            <Route path="/trains" element={<SearchByTypePage />} />
            <Route path="/buses" element={<SearchByTypePage />} />
            <Route path="/flights" element={<SearchByTypePage />} />
            <Route path="/search-results" element={<SearchResultsPage />} />

            <Route element={<ProtectedRoute />}>
              <Route path="/cart" element={<Cart />} />
              <Route path="/account" element={<Account />}>
                <Route path="" element={<AccountDashboard />} />
                <Route path="bookings" element={<Bookings />} />
                <Route
                  path="bookings/:bookingId"
                  element={<BookingDetails />}
                />
                <Route
                  path="billing"
                  element={<BillingPage/>}
                />
                <Route path="passengers" element={<PassengerList />} />
                <Route
                  path="notifications"
                  element={<Notifications/>}
                />
                <Route path="help" element={<Help/>} />
              </Route>
            </Route>

            <Route path="404" element={<NotFoundPage />} />
            <Route path='termsAndConditions' element={<TermsAndConditions/>}/>
            <Route path="*" element={<Navigate to="/404" />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;
