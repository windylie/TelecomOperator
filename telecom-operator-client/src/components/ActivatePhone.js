import React from 'react';
import api from '../apis/telecomOperator';
import CustomerList from './CustomerList';

class ActivatePhone extends React.Component {
    state = { customerId : '', phoneNo : '', response : ''}

    onCustomerIdChange = (customerId) => {
        this.setState({ customerId });
    }

    onPhoneNoChange = (event) => {
        this.setState({ phoneNo : event.target.value });
    }

    onBtnClick = async () => {
        const url = '/customers/' + this.state.customerId + '/phones/' + this.state.phoneNo + '/activation';
        const response = await api.put(url);
        this.setState({response : response.data})
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
                <button className="ui button" onClick={this.onBtnClick}>Activate</button>
            </div>
        );
    }
};

export default ActivatePhone;
