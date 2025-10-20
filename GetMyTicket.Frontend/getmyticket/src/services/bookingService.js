import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "../api/api.js";
import { toast } from "react-toastify";

var userId = localStorage.getItem('userId');

export const Booking = {
    createBooking: (data) => api.post('api/bookings', data),
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
      queryClient.invalidateQueries({queryKey: ['bookings', userId], exact:true});
    },
    onError: (error) => {
      toast.error(error?.message || "Failed to cancel booking");
    },
  });
}

export function useCreateBooking() {
   const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data) => Booking.createBooking(data),
    onSuccess: () => {
      toast.success("Booking was created successfully!");
      queryClient.invalidateQueries({queryKey: ['bookings', userId], exact:true});
    }
  });
}