import { Passenger } from "../../services/passengerService";
import { toast } from "react-toastify";

function PassengerData({ dispatch, passenger, passengerId }) {

    function handleSubmit(e) {
        e.preventDefault();

        let confirmed = window.confirm('By submitting this information, you confirm that it is accurate and as per your personal identification documents. Please be advised that in case of inconsistencies, bording might be denied.')

        if (confirmed) {
            if (passengerId !== null || passengerId !== undefined || passengerId !== "") {
                console.log('IN EDIT', passengerId)
                Passenger.editPassenger(passengerId, passenger)
                    .then(updatedPassenger => {
                        dispatch({ type: 'setPassenger', payload: updatedPassenger })
                    })
            } else {
                Passenger.createPassenger(passenger)
                    .then(updatedPassenger => {
                        dispatch({ type: 'setPassenger', payload: updatedPassenger })
                        dispatch({ type: 'setPassengerId', payload: updatedPassenger })
                    }).catch(err => {
                        toast.error(err.response.data.detail)
                    })
            }
        } else {
            toast.information('Operation aborted.')
        }
    }

    return (
        <div className="passengerDetails">
            <form>

                <div className="passengerDetails__row">
                    <div>
                        <label htmlFor="firstName">First name:</label>
                        <input
                            name="firstName"
                            type="text"
                            placeholder="First name"
                            value={passenger.firstName}
                            onChange={(e) =>
                                dispatch({ type: "setField", field: "firstName", value: e.target.value })
                            }
                        ></input>
                    </div>
                    <div>

                        <label htmlFor="lastName">Last name:</label>
                        <input
                            name="lastName"
                            type="text"
                            placeholder="Last name"
                            value={passenger.lastName}
                            onChange={(e) =>
                                dispatch({ type: "setField", field: "lastName", value: e.target.value })
                            }
                        ></input>
                    </div>

                </div>

                <fieldset>
                    <legend>Gender:</legend>
                    <div>
                        <input
                            checked={passenger.gender === 'Male'}
                            type="radio" name="gender" value="Male"
                            onChange={(e) =>
                                dispatch({ type: "setField", field: "gender", value: e.target.value })
                            }
                        />
                        <label htmlFor="gender">Male</label>
                    </div>
                    <div>
                        <input
                            checked={passenger.gender === 'Female'}
                            type="radio" name="gender" value="Female"
                            onChange={(e) =>
                                dispatch({ type: "setField", field: "gender", value: e.target.value })
                            } />
                        <label htmlFor="gender">Female</label>
                    </div>
                    <div>
                        <input
                            checked={passenger.gender === 'Other'}
                            type="radio" name="gender" value="Other"
                            onChange={(e) =>
                                dispatch({ type: "setField", field: "gender", value: e.target.value })
                            }
                        />
                        <label htmlFor="gender">Other</label>
                    </div>

                </fieldset>

                <div className="passengerDetails__dob">
                    <label htmlFor='dob'>Date of birth:</label>
                    <input type='date' name='dob' value={new Date(passenger.dob).toISOString().split('T')[0]}
                        onChange={(e) =>
                            dispatch({ type: "setField", field: "dob", value: e.target.value })
                        }
                    />
                </div>

                <div className="passengerDetails__docType">
                    <fieldset>
                        <legend>Document type:</legend>
                        <div>
                            <input
                                checked={passenger.documentType === 'Passport'}
                                type="radio" name="documentType" value="Passport"
                                onChange={(e) =>
                                    dispatch({ type: "setField", field: "documentType", value: e.target.value })
                                }
                            />
                            <label htmlFor="documentType">Passport</label>
                        </div>
                        <div>
                            <input
                                checked={passenger.documentType === 'IdCard'}
                                type="radio" name="documentType" value="IdCard"
                                onChange={(e) =>
                                    dispatch({ type: "setField", field: "documentType", value: e.target.value })
                                }
                            />
                            <label htmlFor="documentType">National ID card</label>
                        </div>
                    </fieldset>

                </div>

                <div className="passengerDetails__row">
                    <div>
                        <label htmlFor="nationality">Nationality:</label>
                        <input name='nationality'
                            type="text"
                            placeholder='bulgarian'
                            value={passenger.nationality}
                            onChange={(e) =>
                                dispatch({ type: "setField", field: "nationality", value: e.target.value })
                            }
                        />
                    </div>
                    <div >
                        <label htmlFor="documentNumber">Document Id:</label>
                        <input name='documentNumber'
                            type="text"
                            placeholder='N1526'
                            value={passenger.documentId}
                            onChange={(e) =>
                                dispatch({ type: "setField", field: "documentId", value: e.target.value })
                            }
                        />
                    </div>

                </div>
                <button className="btn passengerDetails__btn"
                    onClick={handleSubmit}
                >Save information</button>
            </form>


            <div className="passengerDetails__notice">
                <h4>Important notice!</h4>
                <p>
                    Please not that if your passport is set to expire less than <b>six months</b> after your trip, you could be denied boarding or entry! Always check the current entry regulations for your destination in advance.
                </p>
                <p>
                    If additional documents are required &#40;such as birth sertificates for passengers under 18 years old, visas etc.&#41;, you will be asked to provide those at the airport. Please note that failure to do may result in denied bording.
                </p>
            </div>

        </div>
    )
}

export default PassengerData;