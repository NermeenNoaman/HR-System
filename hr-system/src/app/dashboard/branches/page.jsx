"use client"

import { useState, useEffect } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardHeader, CardTitle, CardDescription, CardContent } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import {
  getAllBranches,
  getBranchById,
  getBranchesByCompanyId,
  createBranch,
  updateBranch,
  deleteBranch,
  getAllCompanyProfiles,
} from "@/lib/api"

export default function BranchesPage() {
  const [branches, setBranches] = useState([])
  const [companies, setCompanies] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState("")
  const [showForm, setShowForm] = useState(false)
  const [editingBranch, setEditingBranch] = useState(null)
  const [role, setRole] = useState("")
  const [selectedCompanyFilter, setSelectedCompanyFilter] = useState("")
  const [formData, setFormData] = useState({
    companyId: "",
    code: "",
    nameEn: "",
    description: "",
  })
  const [formErrors, setFormErrors] = useState({})

  useEffect(() => {
    if (typeof window !== "undefined") {
      const userRole = localStorage.getItem("role")
      setRole(userRole || "")
    }
    fetchCompanies()
    fetchBranches()
  }, [])

  useEffect(() => {
    if (selectedCompanyFilter) {
      fetchBranchesByCompany(selectedCompanyFilter)
    } else {
      fetchBranches()
    }
  }, [selectedCompanyFilter])

  const fetchCompanies = async () => {
    try {
      const response = await getAllCompanyProfiles()
      setCompanies(response.data || [])
    } catch (err) {
      console.error("Error fetching companies:", err)
    }
  }

  const fetchBranches = async () => {
    try {
      setLoading(true)
      setError("")
      const response = await getAllBranches()
      setBranches(response.data || [])
    } catch (err) {
      console.error("Error fetching branches:", err)
      setError(
        err.response?.data?.message ||
        err.message ||
        "Failed to fetch branches"
      )
    } finally {
      setLoading(false)
    }
  }

  const fetchBranchesByCompany = async (companyId) => {
    try {
      setLoading(true)
      setError("")
      const response = await getBranchesByCompanyId(companyId)
      setBranches(response.data || [])
    } catch (err) {
      console.error("Error fetching branches by company:", err)
      setError(
        err.response?.data?.message ||
        err.message ||
        "Failed to fetch branches"
      )
    } finally {
      setLoading(false)
    }
  }

  const handleInputChange = (e) => {
    const { name, value } = e.target
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }))
    // Clear error for this field
    if (formErrors[name]) {
      setFormErrors((prev) => {
        const newErrors = { ...prev }
        delete newErrors[name]
        return newErrors
      })
    }
  }

  const validateForm = () => {
    const errors = {}

    if (!formData.companyId) {
      errors.companyId = "Company is required"
    }

    if (!formData.code?.trim()) {
      errors.code = "Branch code is required"
    }

    if (!formData.nameEn?.trim()) {
      errors.nameEn = "Branch name is required"
    }

    setFormErrors(errors)
    return Object.keys(errors).length === 0
  }

  const handleSubmit = async (e) => {
    e.preventDefault()

    if (!validateForm()) {
      return
    }

    try {
      setError("")
      if (editingBranch) {
        // Update existing branch
        const updateData = {
          ...formData,
          branchId: editingBranch.branchId,
          companyId: parseInt(formData.companyId),
        }
        await updateBranch(editingBranch.branchId, updateData)
      } else {
        // Create new branch
        const createData = {
          ...formData,
          companyId: parseInt(formData.companyId),
        }
        await createBranch(createData)
      }

      // Reset form and close
      setFormData({
        companyId: "",
        code: "",
        nameEn: "",
        description: "",
      })
      setEditingBranch(null)
      setShowForm(false)
      setFormErrors({})
      
      // Refresh branches based on current filter
      if (selectedCompanyFilter) {
        fetchBranchesByCompany(selectedCompanyFilter)
      } else {
        fetchBranches()
      }
    } catch (err) {
      console.error("Error saving branch:", err)
      setError(
        err.response?.data?.message ||
        err.message ||
        `Failed to ${editingBranch ? "update" : "create"} branch`
      )
    }
  }

  const handleEdit = async (id) => {
    try {
      const response = await getBranchById(id)
      const branch = response.data
      setEditingBranch(branch)
      setFormData({
        companyId: branch.companyId?.toString() || "",
        code: branch.code || "",
        nameEn: branch.nameEn || "",
        description: branch.description || "",
      })
      setFormErrors({})
      setShowForm(true)
    } catch (err) {
      console.error("Error fetching branch:", err)
      setError(
        err.response?.data?.message ||
        err.message ||
        "Failed to fetch branch"
      )
    }
  }

  const handleDelete = async (id) => {
    if (!confirm("Are you sure you want to delete this branch?")) {
      return
    }

    try {
      await deleteBranch(id)
      // Refresh branches based on current filter
      if (selectedCompanyFilter) {
        fetchBranchesByCompany(selectedCompanyFilter)
      } else {
        fetchBranches()
      }
    } catch (err) {
      console.error("Error deleting branch:", err)
      setError(
        err.response?.data?.message ||
        err.message ||
        "Failed to delete branch"
      )
    }
  }

  const handleNew = () => {
    setEditingBranch(null)
    setFormData({
      companyId: selectedCompanyFilter || "",
      code: "",
      nameEn: "",
      description: "",
    })
    setFormErrors({})
    setError("")
    setShowForm(true)
  }

  const getCompanyName = (companyId) => {
    const company = companies.find((c) => c.companyProfileId === companyId)
    return company ? company.nameEn : `Company ID: ${companyId}`
  }

  const isAdmin = role === "admin"
  const canView = role === "admin" || role === "HR"

  if (!canView) {
    return (
      <div className="container mx-auto p-6">
        <Card>
          <CardContent className="p-6">
            <p className="text-destructive">You don't have permission to view this page.</p>
          </CardContent>
        </Card>
      </div>
    )
  }

  return (
    <div className="max-w-7xl mx-auto space-y-6">
      <div className="flex justify-between items-center">
        <div>
          <h1 className="text-4xl font-bold bg-gradient-to-r from-blue-600 via-purple-600 to-blue-600 bg-clip-text text-transparent">
            Company Branches
          </h1>
          <p className="text-gray-600 mt-2">Manage company branch information</p>
        </div>
        {isAdmin && (
          <Button 
            onClick={handleNew}
            className="bg-gradient-to-r from-blue-600 to-purple-600 hover:from-blue-700 hover:to-purple-700 text-white shadow-lg"
          >
            Create New Branch
          </Button>
        )}
      </div>

      {error && !showForm && (
        <div className="p-3 text-sm text-destructive bg-destructive/10 rounded-md border border-destructive/20">
          {error}
        </div>
      )}

      {/* Company Filter */}
      <Card>
        <CardContent className="p-4">
          <div className="flex items-center gap-4">
            <Label htmlFor="companyFilter" className="whitespace-nowrap">
              Filter by Company:
            </Label>
            <select
              id="companyFilter"
              value={selectedCompanyFilter}
              onChange={(e) => setSelectedCompanyFilter(e.target.value)}
              className="flex h-10 w-full rounded-md border border-input bg-background px-3 py-2 text-sm ring-offset-background file:border-0 file:bg-transparent file:text-sm file:font-medium placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50 max-w-xs"
            >
              <option value="">All Companies</option>
              {companies.map((company) => (
                <option key={company.companyProfileId} value={company.companyProfileId}>
                  {company.nameEn}
                </option>
              ))}
            </select>
          </div>
        </CardContent>
      </Card>

      {showForm && (
        <Card>
          <CardHeader>
            <CardTitle>{editingBranch ? "Edit Branch" : "Create Branch"}</CardTitle>
            <CardDescription>
              {editingBranch
                ? "Update the branch information"
                : "Enter the branch information"}
            </CardDescription>
          </CardHeader>
          <CardContent>
            <form onSubmit={handleSubmit} className="space-y-4">
              {error && (
                <div className="p-3 text-sm text-destructive bg-destructive/10 rounded-md border border-destructive/20">
                  {error}
                </div>
              )}

              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div className="space-y-2">
                  <Label htmlFor="companyId">
                    Company <span className="text-destructive">*</span>
                  </Label>
                  <select
                    id="companyId"
                    name="companyId"
                    value={formData.companyId}
                    onChange={handleInputChange}
                    required
                    disabled={!!editingBranch}
                    className="flex h-10 w-full rounded-md border border-input bg-background px-3 py-2 text-sm ring-offset-background file:border-0 file:bg-transparent file:text-sm file:font-medium placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50"
                  >
                    <option value="">Select a company</option>
                    {companies.map((company) => (
                      <option key={company.companyProfileId} value={company.companyProfileId}>
                        {company.nameEn}
                      </option>
                    ))}
                  </select>
                  {formErrors.companyId && (
                    <p className="text-sm text-destructive">{formErrors.companyId}</p>
                  )}
                </div>

                <div className="space-y-2">
                  <Label htmlFor="code">
                    Branch Code <span className="text-destructive">*</span>
                  </Label>
                  <Input
                    id="code"
                    name="code"
                    value={formData.code}
                    onChange={handleInputChange}
                    required
                    placeholder="BR004"
                  />
                  {formErrors.code && (
                    <p className="text-sm text-destructive">{formErrors.code}</p>
                  )}
                </div>

                <div className="space-y-2 md:col-span-2">
                  <Label htmlFor="nameEn">
                    Branch Name (English) <span className="text-destructive">*</span>
                  </Label>
                  <Input
                    id="nameEn"
                    name="nameEn"
                    value={formData.nameEn}
                    onChange={handleInputChange}
                    required
                    placeholder="New Cairo Branch"
                  />
                  {formErrors.nameEn && (
                    <p className="text-sm text-destructive">{formErrors.nameEn}</p>
                  )}
                </div>

                <div className="space-y-2 md:col-span-2">
                  <Label htmlFor="description">Description</Label>
                  <textarea
                    id="description"
                    name="description"
                    value={formData.description}
                    onChange={handleInputChange}
                    placeholder="New branch in New Cairo"
                    rows={3}
                    className="flex min-h-[80px] w-full rounded-md border border-input bg-background px-3 py-2 text-sm ring-offset-background placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50"
                  />
                </div>
              </div>

              <div className="flex gap-2 justify-end">
                <Button
                  type="button"
                  variant="outline"
                  onClick={() => {
                    setShowForm(false)
                    setEditingBranch(null)
                    setFormErrors({})
                    setError("")
                  }}
                >
                  Cancel
                </Button>
                <Button type="submit">
                  {editingBranch ? "Update" : "Create"}
                </Button>
              </div>
            </form>
          </CardContent>
        </Card>
      )}

      <Card>
        <CardHeader>
          <CardTitle>Branches List</CardTitle>
          <CardDescription>
            {branches.length} branch{branches.length !== 1 ? "es" : ""} found
            {selectedCompanyFilter && ` for ${getCompanyName(parseInt(selectedCompanyFilter))}`}
          </CardDescription>
        </CardHeader>
        <CardContent>
          {loading ? (
            <p className="text-center py-8 text-muted-foreground">Loading...</p>
          ) : branches.length === 0 ? (
            <p className="text-center py-8 text-muted-foreground">
              No branches found
            </p>
          ) : (
            <div className="overflow-x-auto">
              <table className="w-full border-collapse">
                <thead>
                  <tr className="border-b">
                    <th className="text-left p-3 font-semibold">ID</th>
                    <th className="text-left p-3 font-semibold">Code</th>
                    <th className="text-left p-3 font-semibold">Branch Name</th>
                    <th className="text-left p-3 font-semibold">Company</th>
                    <th className="text-left p-3 font-semibold">Description</th>
                    {isAdmin && <th className="text-left p-3 font-semibold">Actions</th>}
                  </tr>
                </thead>
                <tbody>
                  {branches.map((branch) => (
                    <tr key={branch.branchId} className="border-b hover:bg-muted/50">
                      <td className="p-3">{branch.branchId}</td>
                      <td className="p-3 font-medium">{branch.code}</td>
                      <td className="p-3">{branch.nameEn}</td>
                      <td className="p-3">{getCompanyName(branch.companyId)}</td>
                      <td className="p-3">{branch.description || "-"}</td>
                      {isAdmin && (
                        <td className="p-3">
                          <div className="flex gap-2">
                            <Button
                              variant="outline"
                              size="sm"
                              onClick={() => handleEdit(branch.branchId)}
                            >
                              Edit
                            </Button>
                            <Button
                              variant="destructive"
                              size="sm"
                              onClick={() => handleDelete(branch.branchId)}
                            >
                              Delete
                            </Button>
                          </div>
                        </td>
                      )}
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </CardContent>
      </Card>
    </div>
  )
}

