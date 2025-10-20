import { createContext, useContext, useState, useEffect } from "react";
import { GetUserBookings } from "../../services/bookingService";
import { useQuery } from "@tanstack/react-query";
import { getUsersPassengers } from "../../services/passengerService";

const AccountContext = createContext();

function useAccountContext() {
  const context = useContext(AccountContext);
  if (!context) {
    throw new Error("useAccountContext must be used within an AccountProvider");
  }
  return context;
}

function AccountProvider({ children }) {
  const [userId, setUserId] = useState(null);

  useEffect(() => {
    const initialId = localStorage.getItem("userId");
    if (initialId) {
      setUserId(initialId);
    }

    const handler = () => {
      const newId = localStorage.getItem("userId");
      setUserId(newId);
    };

    window.addEventListener("userIdSet", handler);
    return () => window.removeEventListener("userIdSet", handler);
  }, []);

  //FETCH ACCOUNT DATA
  const bookingsQuery = useQuery({
    queryKey: ["bookings", userId],
    queryFn: GetUserBookings,
    enabled: !!userId,
  });

  const passengersQuery = useQuery({
    queryKey: ["passengers", userId],
    queryFn: getUsersPassengers,
    enabled: !!userId,
  });

  //TODO -> should also fetch list of PASSENGERS, NOTIFICATIONS and PAYMENT METHODS (currently
  // no entity in the db AND NOT IN USE), associated with user
  return (
    <AccountContext.Provider
      value={{
        userId,
        bookings: bookingsQuery.data || [],
        passengers: passengersQuery.data || [],
        bookingsQuery,
        passengersQuery,
      }}
    >
      {children}
    </AccountContext.Provider>
  );
}

export { useAccountContext, AccountProvider };
