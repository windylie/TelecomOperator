import React from 'react';
import api from '../apis/telecomOperator';

class ActivatePhone extends React.Component {
    state = { customerId : '', phoneNo : '', response : ''}

    onCustomerIdChange = (event) => {
        this.setState({ customerId : event.target.value });
    }

    onPhoneNoChange = (event) => {
        this.setState({ phoneNo : event.target.value });
    }

    onBtnClick = () => {
        const url = '/customers/' + this.state.customerId + '/phones/' + this.state.phoneNo + '/activation';
        console.log(url);
        api.put(url)
            .then(res => {
                console.log(res);
                console.log(res.data);
            });
    }

    render()
    {
        return (
            <div>
                <form className="ui form">
                    <div className="field">
                        <label>CustomerId</label>
                        <input type="text"
                               placeholder="Customer Id"
                               value={this.state.customerId}
                               onChange={this.onCustomerIdChange} />
                    </div>
                    <div className="field">
                        <label>Phone Number</label>
                        <input type="text"
                               placeholder="Phone No"
                               value={this.state.phoneNo}
                               onChange={this.onPhoneNoChange} />
                    </div>
                    <button className="ui button" onClick={this.onBtnClick}>Activate</button>
                </form>
            </div>
        );
    }
};

export default ActivatePhone;
