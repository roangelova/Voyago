export function CreatePriceSummary(
  trip,
  passengersCountForBooking,
  baggage,
  baggagePrices
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

  console.log(baggage, baggagePrices)
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
