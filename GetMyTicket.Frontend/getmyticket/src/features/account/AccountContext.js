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
    const initialId = sessionStorage.getItem("userId");
    if (initialId) {
      setUserId(initialId);
    }

    const handler = () => {
      const newId = sessionStorage.getItem("userId");
      setUserId(newId);
    };

    window.addEventListener("userIdSet", handler);
    return () => window.removeEventListener("userIdSet", handler);
  }, []);

  //FETCH ACCOUNT DATA
  const { data : bookings } = useQuery({
    queryKey: ["bookings", userId],
    queryFn: GetUserBookings
  //  staleTime: 1000 * 60 * 5,
  });

  const { data: passengers } = useQuery({
    queryKey: ["passengers", userId],
    queryFn: getUsersPassengers
  });

  //TODO -> should also fetch list of PASSENGERS, NOTIFICATIONS and PAYMENT METHODS (currently
  // no entity in the db AND NOT IN USE), associated with user
  return (
    <AccountContext.Provider
      value={{
        userId,
        bookings: bookings || [],
        passengers: passengers || []
      }}
    >
      {children}
    </AccountContext.Provider>
  );
}

export { useAccountContext, AccountProvider };
