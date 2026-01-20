import { api } from "../api/api.js";

export const Baggage = {
  getBaggageForBooking: (bookingId) => api.get(`api/Baggage/booking/${bookingId}`),
  getPricesForProvider: (transportationProviderId) => api.get(`api/Baggage/tp/${transportationProviderId}`),
};
