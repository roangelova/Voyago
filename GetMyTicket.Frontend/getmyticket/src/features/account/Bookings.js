function Bookings() {
    return (



        <div className="bookings__container">
            <table>
                <caption>My bookings</caption>
                <tr>
                    <th>Booking reference</th>
                    <th>From</th>
                    <th>To</th>
                    <th>Departure Time</th>
                    <th>Total Price</th>
                    <th>Actions</th>
                </tr>
                <tr>
                    <td>INT787</td>
                    <td>Munich</td>
                    <td>New York</td>
                    <td>19.09.2025, 21:45</td>
                    <td>899 EUR</td>
                    <td>Cancel | Details</td>
                </tr>
                <tr>
                    <td>DEC562</td>
                    <td>Varna</td>
                    <td>Munich</td>
                    <td>01.07.2025, 14:00</td>
                    <td>345 EUR</td>
                    <td>Cancel | Details</td>
                </tr>
                <tr>
                    <td>INT787</td>
                    <td>Munich</td>
                    <td>New York</td>
                    <td>19.09.2025, 21:45</td>
                    <td>899 EUR</td>
                    <td>Cancel | Details</td>
                </tr>
                <tr>
                    <td>DEC562</td>
                    <td>Varna</td>
                    <td>Munich</td>
                    <td>01.07.2025, 14:00</td>
                    <td>345 EUR</td>
                    <td>Cancel | Details</td>
                </tr>
            </table>
        </div>
    );
}

export default Bookings;