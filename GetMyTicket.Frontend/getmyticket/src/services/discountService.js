import { api } from "../api/api.js";

export const Discount = {
    getByName: (discountName) => api.get(`api/discounts/${discountName}`),
    canApplyDiscountToBooking: (passengerId, discountName, bookingCurrentTotal) => api.get(`api/discounts?passengerId=${passengerId}&discountName=${discountName}&bookingCurrentTotal=${bookingCurrentTotal}`)
}