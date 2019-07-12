import React from 'react';
import api from '../apis/telecomOperator';
import CustomerList from './CustomerList';

class CreatePhone extends React.Component {
    state = { customerId : '', phoneNo : '', response : ''}

    onCustomerIdChange = (customerId) => {
        this.setState({ customerId });
    }

    onPhoneNoChange = (event) => {
        this.setState({ phoneNo : event.target.value });
    }

    onBtnClick = () => {
        const url = '/customers/' + this.state.customerId + '/phones/';
        api.post(url, { phoneNo: this.state.phoneNo })
            .then(res => {
                this.setState({ response: 'Successfully added new phone number!'});
            })
            .catch((error) => {
                this.setState({ response: error.response.data.message });
            });
    }

    render() {
        return (
            <div className="ui form">
                <div className="field">
                    <CustomerList onCustomerSelected={this.onCustomerIdChange} />
                </div>
                <div className="field">
                    <label>Phone Number</label>
                    <input type="text"
                            placeholder="Phone No"
                            value={this.state.phoneNo}
                            onChange={this.onPhoneNoChange} />
                </div>
                <button className="ui button" onClick={this.onBtnClick}>Submit</button>
                <div>{this.state.response}</div>
            </div>
        );
    }
};

export default CreatePhone;
