import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "POLICY_LOADALL_SUCCESS":
            return {
                ...state.policies,
                value: action.data,
            }

        default:
            return state
    }
}