import { useActionState } from "react";

function RegisterForm() {
    let userData = {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        address: '',
        subscribedToNewsletter: true
    };

    const registerUser = (formData) => {
        console.log(formData.get('firstName'));
        console.log(formData.get('lastName'));
        console.log(formData.get('email'));
        console.log(formData.get('address'));
        console.log(formData.get('newsletterCheckbox'));
    }

    //TODO: implement registration


    //const [state, formAction, isPending] = useActionState(userData, registerUser, false);


    return (

        <div className="register">
            <form action={registerUser} className="register__container register__form">

                <div className="register__row">
                    <div>
                        <label htmlFor="firstName">First name</label>
                        <input
                            name="firstName"
                            type="text"
                            placeholder="First name"
                            autoComplete="on"
                            autoCapitalize="on"
                        ></input>
                    </div>

                    <div>
                        <label htmlFor="lastName" >Last name</label>
                        <input
                            name="lastName"
                            type="text"
                            placeholder="Last Name"
                            autoComplete="on"
                            autoCapitalize="on"
                        ></input>
                    </div>
                </div>

                <div className="register__row">
                    <div>
                        <label htmlFor="email">Email</label>
                        <input
                            name="email"
                            type="email"
                            placeholder="email@gmail.com"
                            autoComplete="on"
                        ></input>
                    </div>

                    <div>
                        <label htmlFor="password" >Password</label>
                        <input
                            name="password"
                            type="password"
                        ></input>
                    </div>
                </div>

                <div className="register__row-address">
                    <label
                        htmlFor="address">Address</label>
                    <input
                        className="register__row-address"
                        name="address"
                        type="address"></input>
                </div>

                <div className="register__row-checkbox">
                    <label
                        htmlFor="newsletterCheckbox">Send me special offers & other updates by email.</label>
                    <input
                        name="newsletterCheckbox"
                        type="checkbox"
                        defaultChecked="true"></input>
                </div>

                <div className="register__btn">
                    <button
                    >
                        Register
                    </button>
                </div>
            </form>
        </div>

    )
}

export default RegisterForm;