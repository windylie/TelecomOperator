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
        console.log(url);
        api.post(url, { phoneNo: this.state.phoneNo })
            .then(res => {
                console.log(res);
                console.log(res.data);
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
            </div>
        );
    }
};

export default CreatePhone;
