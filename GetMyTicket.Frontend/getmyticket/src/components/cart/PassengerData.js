function PassengerData({ dispatch }) {

    return (
        <div className="passenger-details">
            <h3>My passenger data</h3>
            <form>

                <div>
                    <div>
                        <label htmlFor="firstName">First name:</label>
                        <input
                            name="firstName"
                            type="text"
                            placeholder="First name"
                            value="my placeholder value 1"
                        ></input>
                    </div>

                    <div>
                        <label htmlFor="firstName">Last name:</label>
                        <input
                            name="firstName"
                            type="text"
                            placeholder="First name"
                            value="my placeholder value 2"
                        ></input>
                    </div>
                </div>

                <fieldset>
                    <legend>Gender:</legend>
                    <input type="radio" name="gender" value="Male" />
                    <label for="gender">Male</label>
                    <input type="radio" name="gender" value="Female" />
                    <label for="gender">Female</label>
                    <input type="radio" name="gender" value="Other" />
                    <label for="gender">Other</label>
                </fieldset>

                <div>
                    <label htmlFor='dob'>Date of birth:</label>
                    <input type='date' value={new Date('2000-01-01').toISOString().split('T')[0]} />
                </div>

                <div>
                    <fieldset>
                        <legend>Document type:</legend>
                        <input type="radio" name="documentType" value="Passport" />
                        <label for="documentType">Passport</label>
                        <input type="radio" name="documentType" value="IdCard" />
                        <label for="documentType">National ID card</label>
                    </fieldset>

                    <div>
                        <label htmlFor="documentNumber">Document Id:</label>
                        <input name='documentNumber' type="text" value='12345677' />
                    </div>

                </div>

                <button>Save</button>
            </form>


            <div>
                <h4>Important notice!</h4>
                <p>
                    Please not that if your passport is set to expire less than six months after your trip, you could be denied boarding or entry! Always check the current entry regulations for your destination in advance.
                    If additional documents are required /such as birth sertificates for passengers under 18 years old, visas etc./, you will be asked to provide those at the airport. Please note that failure to do may result in denied bording.
                </p>
            </div>

        </div>
    )
}

export default PassengerData;