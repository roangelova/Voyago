import { api } from "../api/api.js";

export const Passenger = {
    getPassengersForUser: (userId) => api.get(`api/passengers/by-user/${userId}`),
    createPassenger: (data) => api.post('api/passengers', data),
    editPassenger: ( data) => api.put(`api/passengers`, data),
     getNameAndAge: (bookingId) => api.get(`api/passengers/${bookingId}`)
}