export function CreatePriceSummary(
  trip,
  passengersCountForBooking,
  baggage,
  baggagePrices,
  discountApplied,
  discountCode
) {
  let summary = [];

  if (passengersCountForBooking.adults > 0) {
    summary.push(
      `${passengersCountForBooking.adults} Adult ticket${
        passengersCountForBooking.adults > 1 ? "s" : ""
      } — ${trip.adultPrice} ${trip.currency} p.p.`
    );
  }

  if (passengersCountForBooking.children > 0) {
    summary.push(
      `${passengersCountForBooking.children} Child ticket${
        passengersCountForBooking.children > 1 ? "s" : ""
      } — ${trip.childrenPrice} ${trip.currency} p.p.`
    );
  }

  if (passengersCountForBooking.infants > 0) {
    summary.push(
      `${passengersCountForBooking.infants} Infant ticket${
        passengersCountForBooking.infants > 1 ? "s" : ""
      } — Free`
    );
  }

  if (baggage?.length > 0) {
    summary.push("--------");
    baggage.forEach((x) => {
      summary.push(
        `${x.amount} x ${x.type} bag  —  ${
          x.amount * baggagePrices.find((b) => b.size === x.type).price
        } EUR`
      );
    });
  }

  return summary;
}

//calculates the total price for the booking
export function calculateTotalPriceForCart(
  trip,
  passengers,
  baggage,
  baggagePrices,
  discountCode
) {
  let tripPrice =
    passengers.adults * trip.adultPrice +
    passengers.children * trip.childrenPrice;

  if (baggage?.length > 0) {
    baggage.forEach((x) => {
      tripPrice +=
        x.amount * baggagePrices.find((bp) => bp.size === x.type).price;
    });
  }

  if (discountCode) {
    //TODO-> MAKE DYNAMIC; for now, JUST apply a fixed 10% UNTIL WE HAVE SET UP THE be LOGIC
    return tripPrice * 0.9;
  }

  return tripPrice;
}
