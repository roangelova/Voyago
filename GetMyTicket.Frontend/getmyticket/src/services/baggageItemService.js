import { api } from "../api/api.js";

export const BaggageItem = {
    getBaggageForBooking: (bookingId) => api.get(`api/BaggageItems/${bookingId}`),
}