import initialState from "../../../../../initialState";


export default (state = initialState.account, action) => {
    switch (action.type) {

        case "ACCOUNT_SELECT":
            return {
                ...state,
                account:
                {
                    ...state.account,
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