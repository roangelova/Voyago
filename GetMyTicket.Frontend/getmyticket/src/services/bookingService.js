import { useQuery } from "@tanstack/react-query";
import { api } from "../api/api.js";

export const Booking = {
    bookTrip: (data) => api.post('api/bookings', data),
    cancelBooking: (id) => api.put(`api/bookings/${id}`)
}

export async function GetUserBookings({ queryKey }) {
  const [, userId] = queryKey;
  return api.get(`api/bookings/${userId}`);
}