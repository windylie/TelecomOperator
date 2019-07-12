import React from 'react';
import api from '../apis/telecomOperator';
import CustomerList from './CustomerList';

class CustomerPhoneList extends React.Component {
    state = { phoneList : [], customerId: 0, message: "" }

    onInputChange = async (customerId) => {
        const response = await api.get('/customers/' + customerId + '/phones');
        this.setState({ phoneList : response.data, customerId, message:"" });
    }

    onBtnClick = async (phoneNo) => {
        const url = '/customers/' + this.state.customerId + '/phones/' + phoneNo + '/activation';
        await api.put(url)
            .then(res => {
                this.onInputChange(this.state.customerId);
            })
            .catch((error) => {
                this.setState({ message: error.response.data.message });
            });
    }

    getIcon(activated) {
        if (activated)
            return <i className="icon checkmark" />

        return <i className="exclamation icon" />
    }

    getActivateButton(phoneNo, activated) {
        if (!activated)
            return <button className="ui button"
                           onClick={async () => { await this.onBtnClick(phoneNo) }}>
                        Activate
                    </button>

        return null;
    }

    renderPhoneList() {
        const phoneList = this.state.phoneList;

        if (!phoneList || phoneList.length === 0) {
            return (
                <tr>
                    <td colSpan="2">
                        No records found
                    </td>
                </tr>);
        }

        return phoneList.map ((item) => {
            return (
                <tr key={"phone-" + item.id }>
                    <td>{item.phoneNo}</td>
                    <td>{this.getIcon(item.activated)}{item.activated ? "Activated" : "Inactive"}</td>
                    <td>{this.getActivateButton(item.phoneNo, item.activated)}</td>
                </tr>
            );
        });
    }

    render() {
        return (
            <div className="ui form">
                <div className="field">
                    <CustomerList onCustomerSelected={this.onInputChange} />
                </div>
                <table className="ui very basic table">
                    <tbody>
                        {this.renderPhoneList()}
                    </tbody>
                </table>

                <div>{this.state.message}</div>
            </div>
        );
    };

};

export default CustomerPhoneList;
