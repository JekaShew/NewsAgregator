import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "COMPLAINTTYPE_SELECT":
            return {
                ...state,
                editComplaintType:
                {
                    ...state.editComplaintType,
                    [action.name]:
                    {
                        value: action.val,
                    },
                }
            }

        case "COMPLAINTTYPE_LOAD_SUCCESS":
            return {
                ...state,
                editComplaintType:
                {
                    ...state.editComplaintType,
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