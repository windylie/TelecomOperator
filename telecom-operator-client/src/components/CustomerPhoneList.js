import React from 'react';
import api from '../apis/telecomOperator';

class CustomerPhoneList extends React.Component {
    state = { inputCustomerId : '', phoneList : [] }

    onInputChange = (event) => {
        this.setState({ inputCustomerId: event.target.value });
    }

    onBtnClick = async () => {
        const response = await api.get('/customers/' + this.state.inputCustomerId + '/phones');
        this.setState({ phoneList : response.data });
    }

    getIcon(activated)
    {
        if (activated)
            return <i className="icon checkmark" />

        return <i className="exclamation icon" />
    }

    renderPhoneList()
    {
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
                </tr>
            );
        });
    }

    render() {
        return (
            <div>
                <div className="ui action input">
                    <input type="text"
                        placeholder="Input Customer Id..."
                        onChange={this.onInputChange}
                        value={this.state.inputCustomerId} />
                    <button className="ui button"
                            onClick={this.onBtnClick}>
                            Show Phones
                    </button>
                </div>
                <table className="ui very basic table">
                    <tbody>
                        {this.renderPhoneList()}
                    </tbody>
                </table>
            </div>
        );
    };

};

export default CustomerPhoneList;
