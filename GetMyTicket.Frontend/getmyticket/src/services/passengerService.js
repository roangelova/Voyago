import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "../api/api.js";
import { toast } from "react-toastify";

var userId = sessionStorage.getItem('userId');

export const Passenger = {
  getPassengersForUser: (userId) => api.get(`api/passengers/by-user/${userId}`),
  createPassenger: (data) => api.post("api/passengers", data),
  editPassenger: (data) => api.put(`api/passengers`, data),
  getNameAndAge: (bookingId) => api.get(`api/passengers/${bookingId}`),
  deletePassenger: (passengerId) => api.delete(`api/passengers/${passengerId}`),
};

export async function getUsersPassengers({ queryKey }) {
  const [, userId] = queryKey;
  return Passenger.getPassengersForUser(userId);
}

export function useDeletePassenger() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (passengerId) => Passenger.deletePassenger(passengerId),
    onSuccess: () => {
      toast.success("Passenger was deleted successfully!");
      queryClient.invalidateQueries({queryKey: ["passengers", userId], exact: true});
    },
    onError: (error) => {
      toast.error(error?.message || "Failed to delete passenger");
    },
  });
}

export function useCreatePassenger() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data) => Passenger.createPassenger(data),
    onSuccess: () => {
      toast.success("Passenger was created successfully!");
      queryClient.invalidateQueries({queryKey: ["passengers", userId], exact: true});
    },
    onError: (error) => {
      toast.error(error?.message || "Failed to create passenger");
    },
  });
}

export function useEditPassenger() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data) => Passenger.editPassenger(data),
    onSuccess: () => {
      toast.success("Passenger was edited successfully!");
      queryClient.invalidateQueries({queryKey: ["passengers", userId], exact: true});
    },
    onError: (error) => {
      toast.error(error?.message || "Failed to edit passenger");
    },
  });
}

