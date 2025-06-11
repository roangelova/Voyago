import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "../api/api.js";
import { toast } from "react-toastify";

export const Booking = {
    bookTrip: (data) => api.post('api/bookings', data),
    cancelBooking: (id) => api.put(`api/bookings/${id}`)
}

export async function GetUserBookings({ queryKey }) {
  const [, userId] = queryKey;
  return api.get(`api/bookings/${userId}`);
}

export function useCancelBooking() {
   const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id) => Booking.cancelBooking(id),
    onSuccess: () => {
      toast.success("Booking was canceled successfully!");
      queryClient.invalidateQueries(["accountData"]);
    },
    onError: (error) => {
      toast.error(error?.message || "Failed to cancel booking");
    },
  });
}
