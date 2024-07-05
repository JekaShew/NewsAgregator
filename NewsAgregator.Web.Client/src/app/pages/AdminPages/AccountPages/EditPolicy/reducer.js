import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "POLICY_SELECT":
            return {
                ...state,
                editPolicy:
                {
                    ...state.editPolicy,
                    [action.name]:
                    {
                        value: action.val,
                    },
                }
            }

        case "POLICY_LOAD_SUCCESS":
            return {
                ...state,
                editPolicy:
                {
                    ...state.editPolicy,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        }
                    ]))),
                }
            }

        default:
            return state
    }
}