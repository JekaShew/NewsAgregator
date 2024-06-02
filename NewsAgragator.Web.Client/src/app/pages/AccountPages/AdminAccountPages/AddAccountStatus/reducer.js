import initialState from "../../../../../initialState";


export default (state = initialState.accountStatus, action) => {
    switch (action.type) {

        case "ACCOUNTSTATUS_SELECT":
            return {
                ...state,
                accountStatus:
                {
                    ...state.accountStatus,
                    [action.name]:
                    {
                        value: action.val,
                    },
                }
            }

        default:
            return state
    }
}