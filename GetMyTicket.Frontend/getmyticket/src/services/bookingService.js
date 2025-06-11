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

export function CancelBooking(id) {
  const confirm = window.confirm("Do you really want to cancel this booking?");

  if (confirm) {
    Booking.cancelBooking(id)
      .then(() => {
        toast.success("Booking was canceled successfully!");
        return true;
      })
      .catch((err) => {
        toast.error(err);
      });
  } else {
    return false;
  }
}
